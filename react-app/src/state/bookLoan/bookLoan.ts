export interface bookLoan {
    id: string,
    book: {
        id: string,
        isbn: string,
        title: string,
        imageURL: string | null,
        author: {
            firstName: string,
            lastName: string
        }
    }
    loanDate: string,
    dueDate: string
}