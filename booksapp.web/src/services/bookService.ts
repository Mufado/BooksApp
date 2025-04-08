import axios from 'axios';

export interface BookDimensions {
  height: number;
  width: number;
  thickness: number;
}

export interface Book {
  id: string;
  googleBooksId?: string;
  title: string;
  subTitle?: string;
  pageCount: number;
  mainCategory?: string;
  categories?: string[];
  dimensions?: BookDimensions;
  averageRating?: number;
  ratingsCount?: number;
}

export async function getBooks(): Promise<Book[]> {
  try {
    const response = await axios.get<Book[]>('/api/books/all');
    return response.data;
  } catch (error) {
    console.error('Error fetching books:', error);
    throw error; // Re-throw the error to be handled by the caller
  }
}
