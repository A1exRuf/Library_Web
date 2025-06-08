import s from "./MyBooksPage.module.css";
import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../state/store";
import { useEffect } from "react";
import { fetchMyBooks } from "../../state/bookLoan/myBooksSlice";
import { NavLink } from "react-router-dom";
import BookLoanItem from "./BookLoanItem/BookLoanItem";

function MyBooksPage() {
  const bookLoans = useSelector((state: RootState) => state.myBooks);

  const dispatch = useDispatch<AppDispath>();

  useEffect(() => {
    dispatch(fetchMyBooks());
  }, []);

  return (
    <div className={s.container}>
      {!bookLoans.loading && bookLoans.items.length ? (
        <div className={s.bookLoanList}>
          {bookLoans.items.map((item) => (
            <BookLoanItem key={item.id} {...item} />
          ))}
        </div>
      ) : (
        <div className={s.noBookLoans}>
          <h1>
            You haven't borrowed any books yet.{" "}
            <NavLink to="/books">
              Explore our collection and start reading!
            </NavLink>
          </h1>
        </div>
      )}
    </div>
  );
}

export default MyBooksPage;
