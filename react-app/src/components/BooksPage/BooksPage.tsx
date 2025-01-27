import s from "./BookPage.module.css";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchBooks } from "../../state/book/bookListSlice";
import { AppDispath, RootState } from "../../state/store";
import BookItem from "./BookItem/BookItem";
import Search from "./Search/Search";
import useDebounce from "../../hooks/useDebounce";
import Filter from "./Filter/Filter";
import PaginationControls from "./PaginationControls/PaginationControls";
import { useSearchParams } from "react-router-dom";

const BooksPage = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedAuthors, setSelectedAuthors] = useState<string[]>([]);
  const [searchParams, setSearchParams] = useSearchParams();
  const bookList = useSelector((state: RootState) => state.bookList);
  const dispatch = useDispatch<AppDispath>();
  const debouncedSearchTerm = useDebounce(searchTerm, 1000);

  const currentPage = Number(searchParams.get("page")) || 1;

  const handlePageChange = (page: number) => {
    setSearchParams({ page: String(page) });
  };

  const handleSearchChange = (newSearchTerm: string) => {
    setSearchTerm(newSearchTerm);
    handlePageChange(1);
  };

  const handleFilterChange = (authors: string[]) => {
    setSelectedAuthors(authors);
  };

  useEffect(() => {
    dispatch(
      fetchBooks({
        searchTerm: debouncedSearchTerm,
        authorId: selectedAuthors,
        page: currentPage,
      })
    );
  }, [debouncedSearchTerm, selectedAuthors, currentPage, dispatch]);

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
      <PaginationControls
        page={bookList.page}
        pageSize={bookList.pageSize}
        totalCount={bookList.totalCount}
        hasNextPage={bookList.hasNextPage}
        hasPreviousPage={bookList.hasPreviousPage}
        onPageChange={handlePageChange}
      />
    </div>
  );
};

export default BooksPage;
