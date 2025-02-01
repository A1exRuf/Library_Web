import s from "./AddBookPage.module.css";
import { useNavigate } from "react-router-dom";
import useUserRole from "../../hooks/useUserRole";
import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../state/store";
import { useEffect, useState } from "react";
import { fetchAuthors } from "../../state/author/authorListSlice";
import { addBook } from "../../state/book/addBookSlice";

function AddBookPage() {
  const userRole = useUserRole();
  const navigate = useNavigate();

  if (userRole !== "Admin") {
    navigate("/books");
  }

  const genres: string[] = [
    "Fiction",
    "Non-Fiction",
    "Mystery/Thriller",
    "Science Fiction",
    "Fantasy",
    "Biography",
    "Romance",
  ];

  const authorList = useSelector((state: RootState) => state.authorList);
  const { loading, error, success } = useSelector(
    (state: RootState) => state.addBook
  );

  const dispatch = useDispatch<AppDispath>();

  const [isbn, setIsbn] = useState("");
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [genre, setGenre] = useState(genres[0]);
  const [author, setAuthor] = useState(authorList.items[0]?.id || "");
  const [image, setImage] = useState<File | null>(null);

  useEffect(() => {
    dispatch(fetchAuthors());
  }, [dispatch]);

  const formData = new FormData();
  formData.append("Isbn", isbn);
  formData.append("Title", title);
  formData.append("Genree", genre);
  formData.append("Description", description);
  formData.append("AuthorId", author);
  if (image) formData.append("Image", image);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    dispatch(addBook(formData));
  };

  return (
    <div className={s.container}>
      <h1>New book</h1>
      <form onSubmit={handleSubmit}>
        <h2>ISBN:</h2>
        <input
          className={s.input}
          type="text"
          value={isbn}
          onChange={(e) => setIsbn(e.target.value)}
          required
        />

        <h2>Title:</h2>
        <input
          className={s.input}
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

        <button className={s.submit} type="submit" disabled={loading}>
          {loading ? "Adding..." : "Add Book"}
        </button>
      </form>

      {error && <p>{error}</p>}
      {success && <p>Book added successfully!</p>}
    </div>
  );
}

export default AddBookPage;
