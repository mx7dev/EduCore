import { Component, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { form, FormField, required, minLength, submit } from '@angular/forms/signals';
import { ProfesorService, CrearProfesorDto } from '../../../core/services/profesor';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-profesor-form',
  imports: [Button, Dialog, InputText, FormField, Toast],
  providers: [MessageService],
  templateUrl: './profesor-form.html',
  styleUrl: './profesor-form.scss'
})
export class ProfesorForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() profesorGuardado = new EventEmitter<void>();

  private profesorService = inject(ProfesorService);
  private messageService = inject(MessageService);
  guardando = false;

  modelo = signal({
    dni: '',
    nombre: '',
    apellidoPaterno: '',
    apellidoMaterno: '',
    especialidad: '',
    correoElectronico: '',
    numeroCelular: '',
    direccion: ''
  });

  formulario = form(this.modelo, (path) => {
    required(path.dni, { message: 'El DNI es obligatorio' });
    minLength(path.dni, 8, { message: 'El DNI debe tener 8 dígitos' });
    required(path.nombre, { message: 'El nombre es obligatorio' });
    required(path.apellidoPaterno, { message: 'El apellido paterno es obligatorio' });
    required(path.especialidad, { message: 'La especialidad es obligatoria' });
    required(path.correoElectronico, { message: 'El correo es obligatorio' });
  });

  cerrar() {
    this.visible = false;
    this.visibleChange.emit(false);
  }

  guardar() {
    submit(this.formulario, async () => {
      this.guardando = true;
      const v = this.modelo();
      const dto: CrearProfesorDto = {
        dni: v.dni,
        nombre: v.nombre,
        apellidoPaterno: v.apellidoPaterno,
        apellidoMaterno: v.apellidoMaterno || undefined,
        especialidad: v.especialidad,
        correoElectronico: v.correoElectronico,
        numeroCelular: v.numeroCelular || undefined,
        direccion: v.direccion || undefined
      };

      this.profesorService.crear(dto).subscribe({
        next: (response: any) => {
          this.guardando = false;
          if (response.success) {
            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: response.message });
            this.profesorGuardado.emit();
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