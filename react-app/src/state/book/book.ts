interface book {
    id: string,
    isbn: string,
    title: string,
    genree: string,
    description: string,
    authorId: string,
    isAvailable: boolean,
    imageId: string | null,
    loading: boolean,
    error: string,
}

export default book