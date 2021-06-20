export class Movie {
    title: string;
    comment: string;
    rating: number;
    id: number;
    image;

    constructor(id: number, title: string, comment: string, rating: number, image) {
            this.id = id;
            this.title = title;
            this.comment = comment;
            this.rating = rating;
            this.image = image;
        }
}