import s from "./Filter.module.css";
import AuthorFilter from "./AuthorFilter/AuthorFilter";
import GenreFilter from "./GenreFilter/GenreFilter";
import AvailabilityFilter from "./AvailabilityFilter/AvailabilityFilter";

function Filter() {
  return (
    <div className={s.container}>
      <AvailabilityFilter />
      <AuthorFilter />
      <GenreFilter />
    </div>
  );
}

export default Filter;
