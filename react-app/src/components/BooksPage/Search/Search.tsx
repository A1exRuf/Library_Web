import React, { ChangeEvent } from "react";
import s from "./Search.module.css";

interface SearchProps {
  onSearchChange: (searchTerm: string) => void;
}

const Search: React.FC<SearchProps> = ({ onSearchChange }) => {
  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    onSearchChange(event.target.value);
  };

  return (
    <div className={s.container}>
      <input
        className={s.input}
        type="search"
        placeholder="Search"
        onChange={handleChange}
      />
    </div>
  );
};

export default Search;
