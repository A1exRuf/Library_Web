import s from "./PaginationControls.module.css";

interface PaginationControlsProps {
  page: number;
  pageSize: number;
  totalCount: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  onPageChange: (page: number) => void;
}

function PaginationControls({
  page,
  pageSize,
  totalCount,
  hasNextPage,
  hasPreviousPage,
  onPageChange,
}: PaginationControlsProps) {
  const totalPages = Math.ceil(totalCount / pageSize);

  const handlePrevious = () => {
    if (hasPreviousPage) {
      onPageChange(page - 1);
    }
  };

  const handleNext = () => {
    if (hasNextPage) {
      onPageChange(page + 1);
    }
  };

  return (
    <div className={s.container}>
      <button
        className={s.previous_btn}
        onClick={handlePrevious}
        disabled={!hasPreviousPage}
      >
        {"<"}
      </button>
      {[...Array(totalPages)].map((_, index) => (
        <button
          key={index}
          className={`${s.page_btn} ${page === index + 1 ? s.active : ""}`}
          onClick={() => onPageChange(index + 1)}
        >
          {index + 1}
        </button>
      ))}
      <button
        className={s.next_btn}
        onClick={handleNext}
        disabled={!hasNextPage}
      >
        {">"}
      </button>
    </div>
  );
}

export default PaginationControls;
