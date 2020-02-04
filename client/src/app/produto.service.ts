import { Injectable } from '@angular/core';
import { Produto } from './produto/produto';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  apiUrl = 'https://localhost:44319/api/';

  constructor(private http: HttpClient) { }

  getProdutos(): Observable<Produto[]> {
    var data = this.http.get<Produto[]>(this.apiUrl + 'produto');
    return data
      .pipe(
        tap(_ => this.log('fetched produto')),
        catchError(this.handleError('getProdutos', []))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    console.log(message);
  }}
