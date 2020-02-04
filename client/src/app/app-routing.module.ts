import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutoComponent } from './produto/produto.component';
import { LoginComponent } from './auth/login/login.component';


const routes: Routes = [
  {
    path: 'produto',
    component: ProdutoComponent,
    data: { title: 'Lista de Produtos' }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { title: 'Login' }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
