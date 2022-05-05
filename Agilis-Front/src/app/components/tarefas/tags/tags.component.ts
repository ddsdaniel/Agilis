import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { Component, ElementRef, OnInit, ViewChild, Input } from '@angular/core';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { constantes } from 'src/app/consts/constantes';
import { Tag } from 'src/app/models/tags/tag';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { TagApiService } from 'src/app/services/apis/tag-api.service';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.scss']
})
export class TagsComponent implements OnInit {

  @Input() tarefa: Tarefa;
  @ViewChild('tagInput') tagInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto') matAutocomplete: MatAutocomplete;

  todas: Tag[] = [];
  value = '';
  filtradas: Tag[];
  separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(
    private tagApiService: TagApiService,
  ) { }

  ngOnInit(): void {
  }

  public obterTags(): Observable<any> {
    return this.tagApiService.obterTodos()
      .pipe(
        tap(tags => this.todas = tags)
      );
  }

  adicionar(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our tag
    if ((value || '').trim()) {

      const tag: Tag = {
        id: constantes.newGuid,
        nome: value.trim(),
      };

      this.tagApiService.adicionar(tag)
        .subscribe({
          next: id => {
            tag.id = id;
            this.tarefa.tags.push(tag);
          }
        });

    }

    // Reset the input value
    if (input) {
      input.value = '';
    }

    this.value = '';
  }



  selecionou(event: MatAutocompleteSelectedEvent): void {
    const tag: Tag = {
      id: constantes.newGuid,
      nome: event.option.viewValue,
    };
    this.tarefa.tags.push(tag);
    this.tagInput.nativeElement.value = '';
    this.value = '';
  }

  remover(tag: Tag): void {
    const index = this.tarefa.tags.findIndex(t => t.id === tag.id);

    if (index >= 0) {
      this.tarefa.tags.splice(index, 1);
    }
  }

  filtrar() {
    const filterValue = this.value.toLowerCase();
    this.filtradas = this.todas.filter(tag => tag.nome.toLowerCase().indexOf(filterValue) === 0);
  }

}
