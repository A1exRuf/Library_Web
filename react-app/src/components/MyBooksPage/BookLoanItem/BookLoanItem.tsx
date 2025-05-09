import imgBookBlank from "../../../images/book_blank.png";
import s from "./BookLoanItem.module.css";
import { bookLoan } from "../../../state/bookLoan/bookLoan";
import { useDispatch } from "react-redux";
import { AppDispath } from "../../../state/store";
import { returnBook } from "../../../state/bookLoan/myBooksSlice";

function BookLoanItem(props: bookLoan) {
  const dispatch = useDispatch<AppDispath>();

  const loanDate = new Date(props.loanDate).toLocaleDateString();
  const dueDate = new Date(props.dueDate).toLocaleDateString();
  const remainingDays = Math.ceil(
    (new Date(props.dueDate).getTime() - new Date().getTime()) /
      (1000 * 60 * 60 * 24)
  );

  const handleReturnBook = () => {
    dispatch(returnBook(props.id));
  };

  return (
    <div className={s.container}>
      <img src={props.book.imageURL || imgBookBlank} />
      <div className={s.bookLoanInfo}>
        <div className={s.nameGroup}>
          <h1>{props.book.title}</h1>
          <p>
            {"by " +
              props.book.author.firstName +
              " " +
              props.book.author.lastName}
          </p>
        </div>
        <div className={s.dataInfo}>
          <p>Borrowed On: {loanDate}</p>
          <p>Due Date: {dueDate}</p>
          <p>{remainingDays} days remaining</p>
        </div>
      </div>
      <button onClick={handleReturnBook}>Return book</button>
    </div>
  );
}

export default BookLoanItem;
