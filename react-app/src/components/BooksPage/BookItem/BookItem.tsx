import s from "./BookItem.module.css";
import book from "../../../state/book/book";
import imgBookBlank from "../../../images/book_blank.png";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchBookImage } from "../../../state/book/bookImageSlice";
import { AppDispath, RootState } from "../../../state/store";

function BookItem(props: book) {
  const bookImage = useSelector(
    (state: RootState) => state.bookImage[props.imageId || ""]
  );
  const dispatch = useDispatch<AppDispath>();

  useEffect(() => {
    if (props.imageId) {
      dispatch(fetchBookImage(props.imageId));
    }
  }, [props.imageId, dispatch]);

  return (
    <div className={s.bookItem}>
      <img
        src={bookImage?.imageUrl || imgBookBlank}
        alt={props.title + " image"}
        className={s.img}
      />
      <div className={s.bookInfo}>
        <h1>{props.title}</h1>
        <p>
          {props.authorFirstName} {props.authorLastName}
        </p>
        <p>Genre: {props.genree}</p>
        <p>ISBN: {props.isbn}</p>
      </div>
      <button className={s.button}>Go to</button>
    </div>
  );
}

export default BookItem;
