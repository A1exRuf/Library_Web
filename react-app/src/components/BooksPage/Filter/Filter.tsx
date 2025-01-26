import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../../state/store";
import s from "./Filter.module.css";
import { ChangeEvent, useEffect, useState } from "react";
import { fetchAuthors } from "../../../state/author/authorListSlice";

interface FilterProps {
  onFilterChange: (selectedAuthors: string[]) => void;
}

function Filter({ onFilterChange }: FilterProps) {
  const authorList = useSelector((state: RootState) => state.authorList);
  const dispatch = useDispatch<AppDispath>();
  const [selectedAuthors, setSelectedAuthors] = useState<string[]>([]);

  useEffect(() => {
    dispatch(fetchAuthors());
  }, [dispatch]);

  const handleCheckboxChange = (event: ChangeEvent<HTMLInputElement>) => {
    const authorId = event.target.value;
    setSelectedAuthors((prev) =>
      event.target.checked
        ? [...prev, authorId]
        : prev.filter((id) => id !== authorId)
    );
  };

  useEffect(() => {
    onFilterChange(selectedAuthors);
  }, [selectedAuthors, onFilterChange]);

  return (
    <div className={s.container}>
      <h2>Author</h2>
      {authorList.items && authorList.items.length > 0 ? (
        <div>
          {authorList.items.map((author) => (
            <label key={author.id}>
              <input
                type="checkbox"
                value={author.id}
                onChange={handleCheckboxChange}
              />
              {author.firstName + " " + author.lastName}
            </label>
          ))}
        </div>
      ) : (
        <p>No authors available</p>
      )}
    </div>
  );
}

export default Filter;
