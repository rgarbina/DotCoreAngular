import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProdutoService } from '../produto.service';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';


@Component({
  selector: 'app-produto-edit',
  templateUrl: './produto-edit.component.html',
  styleUrls: ['./produto-edit.component.scss']
})

export class ProdutoEditComponent implements OnInit {
  produtoForm: FormGroup;
  id: number = null;
  imagemPath: '';
  nome: '';
  valor: 0.00;
  isLoadingResults = false;
  matcher = new MyErrorStateMatcher();

  constructor(private router: Router, private route: ActivatedRoute, private api: ProdutoService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.getProduto(this.route.snapshot.params['id']);
    this.produtoForm = this.formBuilder.group({
      'id': [null, Validators.required],
      'imagemPath': [null, null],
      'nome': [null, null],
      'valor': [null, null]
    });
  }


  getProduto(id: number) {
    this.api.getProduto(id).subscribe(data => {
      this.id = data.id;
      this.produtoForm.setValue({
        id : data.id,
        imagemPath: data.imagemPath,
        nome: data.nome,
        valor: data.valor
      });
    });
  }

  onFormSubmit(form: NgForm) {
    this.isLoadingResults = true;
    this.api.updateProduto(this.id, form)
      .subscribe(res => {
        this.isLoadingResults = false;
        this.router.navigate(['/produto']);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      }
      );
  }

  produtoDetails() {
    this.router.navigate(['/produto-details', this.id]);
  }

}

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
