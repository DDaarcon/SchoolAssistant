export default interface PreviewLoginsModel {
    administratorUserName: string;
    administratorPassword: string;
    teacherUserName?: string;
    teacherPassword?: string;
    studentUserName?: string;
    studentPassword?: string;
}