import s from "./Filter.module.css";
import AuthorFilter from "./AuthorFilter/AuthorFilter";

function Filter() {
  return (
    <div className={s.container}>
      <AuthorFilter />
    </div>
  );
}

export default Filter;
