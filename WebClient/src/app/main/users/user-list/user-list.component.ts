import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';

import { UserComponent } from './../user/user.component';
// import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { UserService } from '../../services/user.service';
import { UserTypeService } from '../../services/user-type.service';
// import { MatDialog, MatDialogConfig } from "@angular/material";
// import { NotifyService } from '../../../shared/notify.service';
import { DialogService } from '../../../shared/dialog.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator, MatPaginatorIntl } from '@angular/material/paginator';
import { User } from 'src/app/models/user.model';


@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  constructor(private service: UserService,
    private UserTypeService: UserTypeService,
    private dialog: MatDialog,
    // private notificationService: NotifyService,
    private dialogService: DialogService) { }

  listData: MatTableDataSource<User> = new MatTableDataSource<User>;
  displayedColumns: string[] = ['firstname','fullName', 'email', 'mobile', 'city', 'userType', 'actions'];
  @ViewChild(MatSort) sort = new MatSort ;
  @ViewChild(MatPaginator) paginator = MatPaginator;
  searchKey: string = "";
  loading = true;

  ngOnInit() {
    this.service.getUsers().subscribe(
      list => {
        this.loading = false;
        let array = list.map(item => {
          return item;
        });
        this.listData = new MatTableDataSource(array);
        this.listData.sort = this.sort;
        // this.listData.paginator = this.paginator;
        this.listData.filterPredicate = (data, filter) => {
          return this.displayedColumns.some(ele => {
            return ele != 'actions' && data.fullName.toLowerCase().indexOf(filter) != -1;
          });
        };
      });
  }

  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }

  applyFilter() {
    this.listData.filter = this.searchKey.trim().toLowerCase();
  }


  onCreate() {
    this.service.initializeFormGroup();
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    this.dialog.open(UserComponent,dialogConfig);
  }

  onEdit(row: any){
    this.service.populateForm(row);
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    this.dialog.open(UserComponent,dialogConfig);
  }

  onDelete($key: any){
    this.dialogService.openConfirmDialog('Are you sure to delete this record ?')
    .afterClosed().subscribe(res =>{
      if(res){
        // this.service.deleteUser($key);
        // this.notificationService.warn('! Deleted successfully');
      }
    });
  }
}
