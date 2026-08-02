import { Component, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { form, FormField, required, submit } from '@angular/forms/signals';
import { CursoService, CrearCursoDto } from '../../../core/services/curso';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-curso-form',
  imports: [Button, Dialog, InputText, FormField, Toast],
  providers: [MessageService],
  templateUrl: './curso-form.html',
  styleUrl: './curso-form.scss'
})
export class CursoForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() cursoGuardado = new EventEmitter<void>();

  private cursoService = inject(CursoService);
  private messageService = inject(MessageService);
  guardando = false;

  modelo = signal({
    nombre: '',
    descripcion: ''
  });

  formulario = form(this.modelo, (path) => {
    required(path.nombre, { message: 'El nombre del curso es obligatorio' });
  });

  cerrar() {
    this.visible = false;
    this.visibleChange.emit(false);
  }

  guardar() {
    submit(this.formulario, async () => {
      this.guardando = true;
      const v = this.modelo();
      const dto: CrearCursoDto = {
        nombre: v.nombre,
        descripcion: v.descripcion || undefined
      };

      this.cursoService.crear(dto).subscribe({
        next: (response: any) => {
          this.guardando = false;
          if (response.success) {
            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: response.message });
            this.cursoGuardado.emit();
            this.cerrar();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: response.message });
          }
        },
        error: () => {
          this.guardando = false;
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Ocurrió un error inesperado' });
        }
      });
    });
  }
}