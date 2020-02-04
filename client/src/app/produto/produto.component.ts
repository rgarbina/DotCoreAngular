import { Component, OnInit } from '@angular/core';
import { Produto } from './produto';
import { ProdutoService } from '../produto.service';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.scss']
})

export class ProdutoComponent implements OnInit {

  data: Produto[] = [];
  displayedColumns: string[] = ['nome', 'valor', 'imagemPath'];
  isLoadingResults = true;

  constructor(private produtoService: ProdutoService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.getProdutos();
  }

  getProdutos(): void {
    this.produtoService.getProdutos()
      .subscribe(produtos => {
        this.data = produtos;
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }
}
