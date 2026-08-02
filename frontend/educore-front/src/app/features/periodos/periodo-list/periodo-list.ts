import { Component, inject } from '@angular/core';
import { httpResource } from '@angular/common/http';
import { PeriodoService, PeriodoDto } from '../../../core/services/periodo';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { Tag } from 'primeng/tag';
import { PeriodoForm } from '../periodo-form/periodo-form';

@Component({
  selector: 'app-periodo-list',
  imports: [Button, TableModule, Tag, PeriodoForm],
  templateUrl: './periodo-list.html',
  styleUrl: './periodo-list.scss'
})
export class PeriodoList {
  private periodoService = inject(PeriodoService);

  periodosResponse = httpResource<ResponseDto<PeriodoDto[]>>(() => this.periodoService.apiUrl);
  mostrarFormulario = false;

  get periodos(): PeriodoDto[] {
    return this.periodosResponse.value()?.data ?? [];
  }

  abrirFormulario() {
    this.mostrarFormulario = true;
  }
}