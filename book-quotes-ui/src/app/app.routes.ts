import { Routes } from '@angular/router';

import { BooksPage } from './pages/books-page/books-page';

export const routes: Routes = [
  {
    path: 'books',
    component: BooksPage,
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'books',
  },
];
