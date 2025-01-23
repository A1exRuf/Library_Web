import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchBooks } from "../../state/book/bookListSlice";
import { AppDispath, RootState } from "../../state/store";

const BooksPage = () => {
  const bookList = useSelector((state: RootState) => state.bookList);
  const dispatch = useDispatch<AppDispath>();
  useEffect(() => {
    dispatch(fetchBooks());
    console.log(bookList.items);
  }, []);

  return (
    <div>
      {bookList.loading && <div>Loading...</div>}
      {!bookList.loading && bookList.error ? (
        <div>Error: {bookList.error}</div>
      ) : null}
      {!bookList.loading && bookList.items.length ? (
        <ul>
          {bookList.items.map((item) => (
            <li key={item.id}>{item.title}</li>
          ))}
        </ul>
      ) : (
        <h1>NoBooks</h1>
      )}
    </div>
  );
};

export default BooksPage;
