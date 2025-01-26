import author from "../author/author"

interface book {
    id: string,
    isbn: string,
    title: string,
    genree: string,
    description: string,
    author: author,
    isAvailable: boolean,
    imageUrl: string | null,
    loading: boolean,
    error: string,
}

export default book