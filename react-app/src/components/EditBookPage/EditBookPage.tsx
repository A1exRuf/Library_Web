import s from "./EditBookPage.module.css";
import { useNavigate, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../state/store";
import { useEffect, useState } from "react";
import { fetchAuthors } from "../../state/author/authorListSlice";
import { fetchBook, updateBook } from "../../state/book/bookSlice";
import genres from "../../constants/genres";
import useAdminOnly from "../../hooks/useAdminOnly";

interface BookParams {
  id: string;
  [key: string]: string | undefined;
}

function EditBookPage() {
  useAdminOnly();

  const { id } = useParams<BookParams>();
  const navigate = useNavigate();

  const authorList = useSelector((state: RootState) => state.authorList);
  const book = useSelector((state: RootState) => state.book);

  const dispatch = useDispatch<AppDispath>();

  useEffect(() => {
    dispatch(fetchAuthors());
  }, [dispatch]);

  useEffect(() => {
    if (id) {
      dispatch(fetchBook(id));
    }
  }, [dispatch]);

  const [isbn, setIsbn] = useState("");
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [genre, setGenre] = useState(genres[0]);
  const [author, setAuthor] = useState(authorList.items[0]?.id || "");
  const [image, setImage] = useState<File | null>(null);

  useEffect(() => {
    if (book) {
      setIsbn(book.isbn);
      setTitle(book.title);
      setDescription(book.description);
      setGenre(book.genree);
      setAuthor(book.author.id);
    }
  }, [book]);

  if (book.error || !book) {
    return <h1>Book is not found</h1>;
  }

  const formData = new FormData();
  formData.append("BookId", book.id);
  if (isbn !== book.isbn) formData.append("Isbn", isbn);
  if (title !== book.title) formData.append("Title", title);
  if (description !== book.description)
    formData.append("Description", description);
  if (genre !== book.genree) formData.append("Genree", genre);
  if (author !== book.author.id) formData.append("AuthorId", author);
  if (image) formData.append("Image", image);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    dispatch(updateBook({ bookData: formData }));

    if (!book.error) {
      navigate(`/books/${book.id}`);
    }
  };

  return (
    <div className={s.container}>
      <h1>Edit book</h1>
      <form onSubmit={handleSubmit}>
        <h2>ISBN:</h2>
        <input
          className={s.input}
          placeholder={book.isbn}
          type="text"
          value={isbn}
          onChange={(e) => setIsbn(e.target.value)}
          required
        />

        <h2>Title:</h2>
        <input
          className={s.input}
          placeholder={book.title}
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          required
        />

        <h2>Genre:</h2>
        <select
          className={s.input}
          value={genre}
          onChange={(e) => setGenre(e.target.value)}
          required
        >
          {genres.map((genre, index) => (
            <option key={index} value={genre}>
              {genre}
            </option>
          ))}
        </select>

        <h2>Description:</h2>
        <textarea
          className={s.input}
          placeholder={book.description}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          required
        />

        <h2>Author:</h2>
        <select
          className={s.input}
          value={author}
          onChange={(e) => setAuthor(e.target.value)}
          required
        >
          {authorList.items.map((author) => (
            <option key={author.id} value={author.id}>
              {author.firstName + " " + author.lastName}
            </option>
          ))}
        </select>

        <h2>Image:</h2>
        <input
          className={s.input}
          type="file"
          accept="image/*"
          onChange={(e) => e.target.files && setImage(e.target.files[0])}
        />

        <button className={s.submit} type="submit" disabled={book.loading}>
          {book.loading ? "Updating..." : "Update book"}
        </button>
      </form>

      {book.error && <p>{book.error}</p>}
    </div>
  );
}

export default EditBookPage;
