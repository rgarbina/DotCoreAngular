import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { HttpEventType, HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  handleInputChange(e) {
    var file = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];
    var pattern = /image-*/;
    var reader = new FileReader();
    if (!file.type.match(pattern)) {
      alert('invalid format');
      return;
    }
    var outerThis = this;

    reader.readAsDataURL(file);
    reader.onload = function () {
      outerThis.onUploadFinished.emit(reader.result);
    };
  }

}
