export class Movie {
    title: string;
    comment: string;
    rating: number;
    id: number;

    constructor(id: number, title: string, comment: string, rating: number) {
            this.id = id;
            this.title = title;
            this.comment = comment;
            this.rating = rating;
        }
}