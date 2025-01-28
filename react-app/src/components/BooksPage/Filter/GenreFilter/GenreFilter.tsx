import s from "../Filter.module.css";
import { useDispatch } from "react-redux";
import { AppDispath } from "../../../../state/store";
import { setGenre } from "../../../../state/book/filterSlice";

const genres: string[] = [
  "Fiction",
  "Non-Fiction",
  "Mystery/Thriller",
  "Science Fiction",
  "Fantasy",
  "Biography",
  "Romance",
];

function GenreFilter() {
  const dispatch = useDispatch<AppDispath>();

  const handleToggle = (genre: string) => {
    dispatch(setGenre(genre));
  };

  return (
    <div className={s.subContainer}>
      <h2>Genres</h2>
      <div>
        {genres.map((genre) => (
          <label key={genre}>
            <input
              type="checkbox"
              value={genre}
              onChange={() => handleToggle(genre)}
            />
            {genre}
          </label>
        ))}
      </div>
    </div>
  );
}

export default GenreFilter;
