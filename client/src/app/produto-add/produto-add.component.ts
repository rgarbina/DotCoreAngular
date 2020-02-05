import { Component, OnInit } from '@angular/core';
import { ProdutoService } from '../produto.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

@Component({
  selector: 'app-produto-add',
  templateUrl: './produto-add.component.html',
  styleUrls: ['./produto-add.component.scss']
})
export class ProdutoAddComponent implements OnInit {
  public response:"";
  produtoForm: FormGroup;
  imagemPath: '';
  nome: '';
  valor: 0.00;
  isLoadingResults = false;
  matcher = new MyErrorStateMatcher();

  constructor(private router: Router, private api: ProdutoService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.produtoForm = this.formBuilder.group({
      'imagemPath': [null, null],
      'nome': [null, null],
      'valor': [null, null]
    });
  }

  formatCurrency(event) {
    return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(event.target.value);
  }

  onFormSubmit(form: NgForm) {
    this.isLoadingResults = true;
    this.produtoForm.value.imagemPath = this.response;
    const ctrl = new FormControl(this.response);
    this.produtoForm.addControl('imagemPath', ctrl);
    this.api.addProduto(form)
      .subscribe((res: { [x: string]: any; }) => {
        const produto = res['produtoResponse'];
        const id = produto['id'];
        this.isLoadingResults = false;
        this.router.navigate(['/produto-details', id]);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

  public uploadFinished = (event) => {
    this.response = event;
  }
}

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
