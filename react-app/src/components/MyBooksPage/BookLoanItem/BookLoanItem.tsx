import imgBookBlank from "../../../images/book_blank.png";
import s from "./BookLoanItem.module.css";
import { bookLoan } from "../../../state/bookLoan/bookLoan";

function BookLoanItem(props: bookLoan) {
  const loanDate = new Date(props.loanDate).toLocaleDateString();
  const dueDate = new Date(props.dueDate).toLocaleDateString();
  const remainingDays = Math.ceil(
    (new Date(props.dueDate).getTime() - new Date().getTime()) /
      (1000 * 60 * 60 * 24)
  );

  return (
    <div className={s.container}>
      <div className={s.bookInfo}>
        <h1>{props.book.title}</h1>
        <p>
          {"by " +
            props.book.author.firstName +
            " " +
            props.book.author.lastName}
        </p>
        <p>Borrowed On: {loanDate}</p>
        <p>Due Date: {dueDate}</p>
        <p>{remainingDays} days remaining</p>
        <p> </p>
      </div>
      <img src={props.book.imageURL || imgBookBlank} />
    </div>
  );
}

export default BookLoanItem;
