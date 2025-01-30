import s from "./MyBooksPage.module.css";
import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../state/store";
import { useEffect } from "react";
import { fetchMyBooks } from "../../state/bookLoan/myBooksSlice";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";
import BookLoanItem from "./BookLoanItem/BookLoanItem";

function MyBooksPage() {
  const bookLoans = useSelector((state: RootState) => state.myBooks);
  const accessToken = useSelector(
    (state: RootState) => state.signIn.accessToken
  );

  const dispatch = useDispatch<AppDispath>();
  const navigate = useNavigate();

  useEffect(() => {
    if (!accessToken) {
      navigate("/signin");
      return;
    }
    const userId = jwtDecode(accessToken).sub;

    if (userId) {
      dispatch(fetchMyBooks({ userId: userId, page: 1 }));
    }
  }, [accessToken, dispatch, navigate]);

  return (
    <div>
      <div className={s.content}>
        {bookLoans.loading && <div>Loading...</div>}
        {!bookLoans.loading && bookLoans.error ? (
          <div>Error: {bookLoans.error}</div>
        ) : null}
        {!bookLoans.loading && bookLoans.items.length ? (
          <div className={s.bookList}>
            {bookLoans.items.map((item) => (
              <BookLoanItem key={item.id} {...item} />
            ))}
          </div>
        ) : (
          <h1>No books available</h1>
        )}
      </div>
    </div>
  );
}

export default MyBooksPage;
