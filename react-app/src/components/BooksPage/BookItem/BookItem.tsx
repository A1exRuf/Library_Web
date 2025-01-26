import s from "./BookItem.module.css";
import book from "../../../state/book/book";
import imgBookBlank from "../../../images/book_blank.png";

function BookItem(props: book) {
  return (
    <div className={s.bookItem}>
      <img
        src={props.imageUrl || imgBookBlank}
        alt={props.title + " image"}
        className={s.img}
      />
      <div className={s.bookInfo}>
        <h1>{props.title}</h1>
        <p>
          {props.author.firstName} {props.author.lastName}
        </p>
        <p>Genre: {props.genree}</p>
        <p>ISBN: {props.isbn}</p>
      </div>
      <button className={s.button}>Go to</button>
    </div>
  );
}

export default BookItem;
