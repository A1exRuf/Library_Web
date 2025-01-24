interface book {
    id: string,
    isbn: string,
    title: string,
    genree: string,
    description: string,
    authorId: string,
    authorFirstName: string,
    authorLastName: string,
    authorDateOfBirth: string,
    authorCountry: string,
    isAvailable: boolean,
    imageId: string | null,
    loading: boolean,
    error: string,
}

export default book