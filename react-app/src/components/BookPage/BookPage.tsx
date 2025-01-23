import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../state/store";

const BookPage = () => {
  const bookTitle = useSelector((state: RootState) => state.book.title);
  const dispatch = useDispatch<AppDispath>();

  return (
    <div>
      <h1>{bookTitle}</h1>
    </div>
  );
};

export default BookPage;
