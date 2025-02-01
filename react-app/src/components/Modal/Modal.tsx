import React from "react";
import s from "./Modal.module.css";

interface ModalProps {
  isOpen: boolean;
  message: string;
  onConfirm: () => void;
  onCancel: () => void;
}

const Modal: React.FC<ModalProps> = ({
  isOpen,
  message,
  onConfirm,
  onCancel,
}) => {
  if (!isOpen) return null;

  return (
    <div className={s.overlay}>
      <div className={s.modal}>
        <h2>{message}</h2>
        <div className={s.buttons}>
          <button className={s.cancelButton} onClick={onCancel}>
            Cancel
          </button>
          <button className={s.confirmButton} onClick={onConfirm}>
            Confirm
          </button>
        </div>
      </div>
    </div>
  );
};

export default Modal;
