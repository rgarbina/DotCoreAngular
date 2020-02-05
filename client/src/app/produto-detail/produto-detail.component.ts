import { Component, OnInit } from '@angular/core';
import { Produto } from '../produto/produto';
import { ProdutoService } from '../produto.service';
import { AuthService } from '../auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-produto-detail',
  templateUrl: './produto-detail.component.html',
  styleUrls: ['./produto-detail.component.scss']
})
export class ProdutoDetailComponent implements OnInit {
  produto: Produto = {
    id: 0,
    imagemPath: '',
    nome: '',
    valor: 0.00,
  };
  isLoadingResults = true;

  constructor(private produtoService: ProdutoService, private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getProdutoDetails(this.route.snapshot.params['id']);
  }

  getProdutoDetails(id) {
    this.produtoService.getProduto(id)
      .subscribe(data => {
        this.produto = data;
        console.log(this.produto);
        this.isLoadingResults = false;
      });
  }

  public createImgPath = (serverPath: string) => {
    return `https://localhost:44319/${serverPath}`;
  }

  deleteProduto(id: number) {
    this.isLoadingResults = true;
    this.produtoService.deleteProduto(id)
      .subscribe(res => {
        this.isLoadingResults = false;
        this.router.navigate(['/produto']);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      }
      );
  }
}
