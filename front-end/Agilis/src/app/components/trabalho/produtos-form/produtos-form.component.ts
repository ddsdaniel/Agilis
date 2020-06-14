import { Component, OnInit } from '@angular/core';
import { constantes } from 'src/app/constants/constantes';
import { TimesApiService } from 'src/app/services/api/pessoas/times-api.service';
import { Produto } from 'src/app/models/trabalho/produto';

@Component({
  selector: 'app-produtos-form',
  templateUrl: './produtos-form.component.html',
  styleUrls: ['./produtos-form.component.scss']
})
export class ProdutosFormComponent implements OnInit {

  produto: Produto;
  numeroSprint: number;
  nomeSprint: string;

  constructor(
    private timesApiService: TimesApiService,
  ) { }

  ngOnInit() {
    // this.timesApiService.obterTodos()
    //   .subscribe(times => this.times = times);
  }

  // salvar() {
  //   //this.entidade.time = this.times.find(t => t.id === this.entidade.time.id);
  //   super.salvar();
  // }

  salvar(): void {

    // this.timesApiService.alterarProduto(this.entidade.id, this.entidade)
    //   .subscribe(
    //     () => this.router.navigateByUrl(this.rotaPesquisa),
    //     (error: HttpErrorResponse) => {
    //       console.log(error);
    //       this.snackBar.open(error.message);
    //     }
    //   );
  }
}
