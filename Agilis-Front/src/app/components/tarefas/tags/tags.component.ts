import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { TarefaApiService } from 'src/app/services/apis/tarefa-api.service';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.scss']
})
export class TagsComponent implements OnInit {

  @Input() tarefa: Tarefa;
  @ViewChild('tagInput') tagInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto') matAutocomplete: MatAutocomplete;

  todas: string[] = [];
  value = '';
  filtradas: string[];
  separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(
    private tarefaApiService: TarefaApiService,
  ) { }

  ngOnInit(): void {
  }

  public obterTags(): Observable<any> {
    return this.tarefaApiService.obterTags()
      .pipe(
        tap(tags => this.todas = tags)
      );
  }

  adicionar(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our tag
    if ((value || '').trim()) {
      const tag = value.trim();
      this.tarefa.tags.push(tag);
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }

    this.value = '';
  }



  selecionou(event: MatAutocompleteSelectedEvent): void {
    const tag = event.option.viewValue;
    this.tarefa.tags.push(tag);
    this.tagInput.nativeElement.value = '';
    this.value = '';
  }

  remover(tag: string): void {
    const index = this.tarefa.tags.findIndex(t => t.toLowerCase() === tag.toLowerCase());

    if (index >= 0) {
      this.tarefa.tags.splice(index, 1);
    }
  }

  filtrar() {
    const filterValue = this.value.toLowerCase();
    this.filtradas = this.todas.filter(tag => tag.toLowerCase().indexOf(filterValue) === 0);
  }

}
