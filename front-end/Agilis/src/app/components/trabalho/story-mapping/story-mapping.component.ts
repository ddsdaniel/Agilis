import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { TemaSM } from 'src/app/models/trabalho/stories-mapping/tema-sm';

@Component({
  selector: 'app-story-mapping',
  templateUrl: './story-mapping.component.html',
  styleUrls: ['./story-mapping.component.scss']
})
export class StoryMappingComponent implements OnInit {

  temas: TemaSM[] = [
    {
      nome: 'Estoque',
      epicos: [
        {
          nome: 'Cad. Produtos',
          userStories: [
            {
              nome: 'Quero cadastrar um produto genérico, sem preço e que solicite supervisor porque eu sou desconfiado',
            },
            {
              nome: 'Quero alterar o preço',
            },
            {
              nome: 'Excluir um produto',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      nome: 'Fiscal',
      epicos: [
        {
          nome: 'Apuração de Impostos',
          userStories: [
            {
              nome: 'PIS',
            },
            {
              nome: 'COFINS',
            },
            {
              nome: 'ICMS',
            }
          ]
        },
        {
          nome: 'SPED',
          userStories: [
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            },
            {
              nome: 'C100',
            },
            {
              nome: 'C170',
            },
            {
              nome: '0200',
            }
            ,
            {
              nome: 'D100',
            }
          ]
        }
      ]
    }
  ];

  constructor() { }

  ngOnInit() {
  }


  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }
}
