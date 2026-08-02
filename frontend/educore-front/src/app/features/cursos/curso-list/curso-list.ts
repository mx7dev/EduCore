import { Component, inject } from '@angular/core';
import { httpResource } from '@angular/common/http';
import { CursoService, CursoDto } from '../../../core/services/curso';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { CursoForm } from '../curso-form/curso-form';

@Component({
  selector: 'app-curso-list',
  imports: [Button, TableModule, CursoForm],
  templateUrl: './curso-list.html',
  styleUrl: './curso-list.scss'
})
export class CursoList {
  private cursoService = inject(CursoService);
  
  cursosResponse = httpResource<ResponseDto<CursoDto[]>>(() => this.cursoService.apiUrl);
  mostrarFormulario = false;

  get cursos(): CursoDto[] {
    return this.cursosResponse.value()?.data ?? [];
  }

  abrirFormulario() {
    this.mostrarFormulario = true;
  }

  eliminar(id: number) {
    if (confirm('¿Estás seguro de eliminar este curso?')) {
      this.cursoService.eliminar(id).subscribe({
        next: () => this.cursosResponse.reload()
      });
    }
  }
}