import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Produto } from 'src/app/models/produtos/produto';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-product-backlog',
  templateUrl: './product-backlog.component.html',
  styleUrls: ['./product-backlog.component.scss']
})
export class ProductBacklogComponent implements OnInit {

  produto: Produto;
  titulo = 'Product Backlog';

  constructor(
    private activatedRoute: ActivatedRoute,
    private produtoApiService: ProdutoApiService,
    private tituloService: TituloService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      () => {

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.produtoApiService.obterUm(id)
          .subscribe({
            next: produto => {
              this.produto = produto;
              this.titulo = `${produto.nome} - Product Backlog`;
              this.tituloService.definir(this.titulo);
            }
          });
      });
  }

  adicionarEpico() {
    const params = {
      queryParams: {
        produtoId: this.produto.id
      }
    };

    this.router.navigate(['/produtos/epicos/new'], params);
  }
}
