import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../../../state/store";
import { useEffect } from "react";
import { fetchAuthors } from "../../../../state/author/authorListSlice";
import { setAuthorId } from "../../../../state/book/filterSlice";

function AuthorFilter() {
  const authorList = useSelector((state: RootState) => state.authorList);

  const dispatch = useDispatch<AppDispath>();

  const handleToggle = (authorId: string) => {
    dispatch(setAuthorId(authorId));
  };

  useEffect(() => {
    dispatch(fetchAuthors());
  }, [dispatch]);

  return (
    <div>
      <h2>Author </h2>
      {authorList.items && authorList.items.length > 0 ? (
        <div>
          {authorList.items.map((author) => (
            <label key={author.id}>
              <input
                type="checkbox"
                value={author.id}
                onChange={() => handleToggle(author.id)}
              />
              {author.firstName + " " + author.lastName}
            </label>
          ))}
        </div>
      ) : (
        <p>No authors available </p>
      )}
    </div>
  );
}

export default AuthorFilter;
