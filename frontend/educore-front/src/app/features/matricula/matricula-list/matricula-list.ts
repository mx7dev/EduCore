import { Component, inject } from '@angular/core';
import { httpResource } from '@angular/common/http';
import { MatriculaService, MatriculaDto } from '../../../core/services/matricula';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { Tag } from 'primeng/tag';
import { Tooltip } from 'primeng/tooltip';
import { DatePipe } from '@angular/common';
import { MatriculaForm } from '../matricula-form/matricula-form';

@Component({
  selector: 'app-matricula-list',
  imports: [Button, TableModule, Tag, Tooltip, DatePipe, MatriculaForm],
  templateUrl: './matricula-list.html',
  styleUrl: './matricula-list.scss'
})
export class MatriculaList {
  private matriculaService = inject(MatriculaService);
  
  matriculasResponse = httpResource<ResponseDto<MatriculaDto[]>>(() => this.matriculaService.apiUrl);
  mostrarFormulario = false;

  get matriculas(): MatriculaDto[] {
    return this.matriculasResponse.value()?.data ?? [];
  }

  abrirFormulario() {
    this.mostrarFormulario = true;
  }

  anular(id: number) {
    if (confirm('¿Estás seguro de anular esta matrícula?')) {
      this.matriculaService.anular(id).subscribe({
        next: () => this.matriculasResponse.reload()
      });
    }
  }
}