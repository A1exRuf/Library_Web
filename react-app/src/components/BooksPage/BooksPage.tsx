import s from "./BookPage.module.css";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchBooks } from "../../state/book/bookListSlice";
import { AppDispath, RootState } from "../../state/store";
import BookItem from "./BookItem/BookItem";
import Search from "./Search/Search";
import useDebounce from "../../hooks/useDebounce";

const BooksPage = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const bookList = useSelector((state: RootState) => state.bookList);
  const dispatch = useDispatch<AppDispath>();
  const debouncedSearchTerm = useDebounce(searchTerm, 1000);

  useEffect(() => {
    dispatch(fetchBooks(debouncedSearchTerm));
  }, [debouncedSearchTerm, dispatch]);

  const handleSearchChange = (newSearchTerm: string) => {
    setSearchTerm(newSearchTerm);
  };

  return (
    <>
      <Search onSearchChange={handleSearchChange} />
      <div>
        {bookList.loading && <div>Loading...</div>}
        {!bookList.loading && bookList.error ? (
          <div>Error: {bookList.error}</div>
        ) : null}
        {!bookList.loading && bookList.items.length ? (
          <div className={s.bookList}>
            {bookList.items.map((item) => (
              <BookItem key={item.id} {...item} />
            ))}
          </div>
        ) : (
          <h1>No books available</h1>
        )}
      </div>
    </>
  );
};

export default BooksPage;
