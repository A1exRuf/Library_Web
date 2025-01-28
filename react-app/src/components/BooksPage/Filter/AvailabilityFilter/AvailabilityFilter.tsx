import s from "../Filter.module.css";
import { useDispatch } from "react-redux";
import { AppDispath } from "../../../../state/store";
import { setAvailability } from "../../../../state/book/filterSlice";

function AvailabilityFilter() {
  const dispatch = useDispatch<AppDispath>();

  const handleToggle = () => {
    dispatch(setAvailability());
  };

  return (
    <div className={s.subContainer}>
      <label>
        <input type="checkbox" onChange={() => handleToggle()} />
        Show unavailable
      </label>
    </div>
  );
}

export default AvailabilityFilter;
