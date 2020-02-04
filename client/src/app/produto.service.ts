import { Injectable } from '@angular/core';
import { Produto } from './produto/produto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const apiUrl = 'https://localhost:44319/api/Produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  constructor(private http: HttpClient) { }

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
  }

  getProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(apiUrl, httpOptions)
      .pipe(
        tap(heroes => console.log('fetched Suppliers')),
        catchError(this.handleError('getProdutos', []))
      );
  }

  getProduto(id: number): Observable<Produto> {
    const url = `${apiUrl}/${id}`;
    return this.http.get<Produto>(url, httpOptions).pipe(
      tap(_ => console.log(`fetched Produto id=${id}`)),
      catchError(this.handleError<Produto>(`getProduto id=${id}`))
    );
  }

  addProduto(produto: any): Observable<Produto> {
    console.log(JSON.stringify(produto));
    return this.http.post<Produto>(apiUrl, JSON.stringify(produto), httpOptions).pipe(
      tap((produtoRes: Produto) => console.log(`added Produto w/ id=${produtoRes.id}`)),
      catchError(this.handleError<Produto>('addProduto'))
    );
  }

  updateProduto(id: number, produto: any): Observable<any> {
    console.log(id);
    const url = `${apiUrl}/${id}`;
    console.log(url);
    return this.http.put(url, JSON.stringify(produto), httpOptions).pipe(
      tap(_ => console.log(`updated Produto id=${id}`)),
      catchError(this.handleError<any>('updateProduto'))
    );
  }

  deleteProduto(id: number): Observable<Produto> {
    const url = `${apiUrl}/${id}`;
    return this.http.delete<Produto>(url, httpOptions).pipe(
      tap(_ => console.log(`deleted Produto id=${id}`)),
      catchError(this.handleError<Produto>('deleteProduto'))
    );
  }

}
