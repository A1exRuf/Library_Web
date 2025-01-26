import s from "./BookPage.module.css";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchBooks } from "../../state/book/bookListSlice";
import { AppDispath, RootState } from "../../state/store";
import BookItem from "./BookItem/BookItem";
import Search from "./Search/Search";
import useDebounce from "../../hooks/useDebounce";
import Filter from "./Filter/Filter";

const BooksPage = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedAuthors, setSelectedAuthors] = useState<string[]>([]);
  const bookList = useSelector((state: RootState) => state.bookList);
  const dispatch = useDispatch<AppDispath>();
  const debouncedSearchTerm = useDebounce(searchTerm, 1000);

  useEffect(() => {
    dispatch(
      fetchBooks({
        searchTerm: debouncedSearchTerm,
        authorId: selectedAuthors,
      })
    );
  }, [debouncedSearchTerm, selectedAuthors, dispatch]);

  const handleSearchChange = (newSearchTerm: string) => {
    setSearchTerm(newSearchTerm);
  };

  const handleFilterChange = (authors: string[]) => {
    setSelectedAuthors(authors);
  };

  return (
    <div className={s.container}>
      <Search onSearchChange={handleSearchChange} />
      <Filter onFilterChange={handleFilterChange} />
      <div className={s.content}>
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
    </div>
  );
};

export default BooksPage;
