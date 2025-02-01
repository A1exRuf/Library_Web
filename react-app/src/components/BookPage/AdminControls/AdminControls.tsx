import { useState } from "react";
import useUserRole from "../../../hooks/useUserRole";
import s from "./AdminControls.module.css";
import Modal from "../../Modal/Modal";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { AppDispath } from "../../../state/store";
import { deleteBook } from "../../../state/book/bookSlice";

function AdminControls(props: { bookId: string }) {
  const userRole = useUserRole();
  const [isModalOpen, setIsModalOpen] = useState(false);

  if (userRole !== "Admin") return null;

  const dispatch = useDispatch<AppDispath>();
  const navigate = useNavigate();

  const handleDelete = () => {
    dispatch(deleteBook(props.bookId));
    navigate("/books");
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const openModal = () => {
    setIsModalOpen(true);
  };

  return (
    <div className={s.adminControls}>
      <button className={s.edit}>Edit</button>
      <button className={s.remove} onClick={openModal}>
        Remove
      </button>

      <Modal
        isOpen={isModalOpen}
        message="Are you sure you want to delete this book?"
        onConfirm={handleDelete}
        onCancel={handleCancel}
      />
    </div>
  );
}

export default AdminControls;
