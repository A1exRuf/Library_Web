import imgBookBlank from "../../images/book_blank.png";
import s from "./bookPage.module.css";
import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../state/store";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect } from "react";
import { fetchBook } from "../../state/book/bookSlice";
import { takeBook } from "../../state/bookLoan/myBooksSlice";
import { jwtDecode } from "jwt-decode";
import AdminControls from "./AdminControls/AdminControls";

interface BookParams {
  id: string;
  [key: string]: string | undefined;
}

function BookPage() {
  const book = useSelector((state: RootState) => state.book);
  const accessToken = useSelector(
    (state: RootState) => state.signIn.accessToken
  );

  const dispatch = useDispatch<AppDispath>();
  const navigate = useNavigate();

  const { id } = useParams<BookParams>();

  if (id) {
    useEffect(() => {
      dispatch(fetchBook(id));
    }, [dispatch]);
  }

  const handleTakeBook = () => {
    if (accessToken) {
      const userId = jwtDecode(accessToken).sub;
      if (userId) {
        dispatch(takeBook({ bookId: book.id, userId: userId }));
        navigate("/books");
      }
    } else {
      navigate("/signin");
    }
  };

  return (
    <div className={s.container}>
      <img
        src={book.imageUrl || imgBookBlank}
        alt={book.title + " image"}
        className={s.img}
      />
      <div className={s.bookInfo}>
        <div className={s.titleContainer}>
          <h1 className={s.title}>{book.title}</h1>
          <h2 className={book.isAvailable ? s.available : s.notAvailable}>
            {book.isAvailable ? "Available" : "Not available"}
          </h2>
        </div>
        <p className={s.author}>
          by {book.author.firstName} {book.author.lastName}
        </p>
        <p className={s.genre}>Genre: {book.genree}</p>
        <p className={s.isbn}>ISBN: {book.isbn}</p>
        <p className={s.description}>{book.description}</p>
        <p className={s.country}>Country: {book.author.country}</p>
        <div className={s.buttonContainer}>
          {book.isAvailable && (
            <button className={s.button} onClick={handleTakeBook}>
              Take
            </button>
          )}
          <AdminControls bookId={book.id} />
        </div>
      </div>
    </div>
  );
}

export default BookPage;
