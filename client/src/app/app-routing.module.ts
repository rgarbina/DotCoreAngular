import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './auth/login/login.component';
import { ProdutoComponent } from './produto/produto.component';
import { ProdutoDetailComponent } from './produto-detail/produto-detail.component';
import { ProdutoAddComponent } from './produto-add/produto-add.component';
import { ProdutoEditComponent } from './produto-edit/produto-edit.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    data: { title: 'Login' }
  },
  {
    path: 'produto',
    component: ProdutoComponent,
    data: { title: 'Lista de Produtos' }
  },
  {
    path: 'produto-details/:id',
    component: ProdutoDetailComponent,
    data: { title: 'Detalhe do Produto' }
  },
  {
    path: 'produto-add',
    component: ProdutoAddComponent,
    data: { title: 'Novo Produto' }
  },
  {
    path: 'produto-edit/:id',
    component: ProdutoEditComponent,
    data: { title: 'Editar Produto' }
  },
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
