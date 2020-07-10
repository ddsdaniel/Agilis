import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { Tema } from 'src/app/models/trabalho/temas/tema';
import { constantes } from 'src/app/constants/constantes';

@Component({
  selector: 'app-story-mapping',
  templateUrl: './story-mapping.component.html',
  styleUrls: ['./story-mapping.component.scss']
})
export class StoryMappingComponent implements OnInit {

  temas: Tema[] = [
    {
      id: constantes.newGuid,
      nome: 'Estoque',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Cad. Produtos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'Quero cadastrar um produto genérico, sem preço e que solicite supervisor porque eu sou desconfiado',
            },
            {
              id: constantes.newGuid,
              nome: 'Quero alterar o preço',
            },
            {
              id: constantes.newGuid,
              nome: 'Excluir um produto',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            },
            {
              id: constantes.newGuid,
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
              nome: 'D100',
            }
          ]
        }
      ]
    },
    {
      id: constantes.newGuid,
      nome: 'Fiscal',
      epicos: [
        {
          id: constantes.newGuid,
          nome: 'Apuração de Impostos',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'PIS',
            },
            {
              id: constantes.newGuid,
              nome: 'COFINS',
            },
            {
              id: constantes.newGuid,
              nome: 'ICMS',
            }
          ]
        },
        {
          id: constantes.newGuid,
          nome: 'SPED',
          userStories: [
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
              nome: 'D100',
            },
            {
              id: constantes.newGuid,
              nome: 'C100',
            },
            {
              id: constantes.newGuid,
              nome: 'C170',
            },
            {
              id: constantes.newGuid,
              nome: '0200',
            }
            ,
            {
              id: constantes.newGuid,
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
