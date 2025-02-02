import s from "./BookItem.module.css";
import book from "../../../state/book/book";
import imgBookBlank from "../../../images/book_blank.png";
import { NavLink } from "react-router-dom";

function BookItem(props: book) {
  return (
    <div className={s.bookItem}>
      <img
        src={props.imageUrl || imgBookBlank}
        alt={props.title + " image"}
        className={s.img}
      />
      <div className={s.bookInfo}>
        <div className={s.nameGroup}>
          <h2>{props.title}</h2>
          <p>
            by {props.author.firstName} {props.author.lastName}
          </p>
        </div>
        <p>Genre: {props.genree}</p>
        <p>ISBN: {props.isbn}</p>
      </div>
      <NavLink to={`/books/${props.id}`} className={s.button}>
        Go to
      </NavLink>
    </div>
  );
}

export default BookItem;
