var table = $('#userListTable').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": function (data, callback, settings) {

        //data的参数请参考： https://segmentfault.com/a/1190000004478726
        var param = {};
        param.draw = data.draw;
        param.pageNum = (data.start / data.length) + 1;
        param.pageSize = data.length;

        param.sex = $('#select-sex option:selected').val();
        param.phone = $('#input-phone').val();
        param.name = $('#input-name').val();
        $.ajax({
            type: "GET",
            data: param,
            url: "/Account/UserList?handler=UserPage",
            dataType: "json",
            success: function (data) {
                //成功后回调自动渲染
                callback(data);
            }
        });
    },
    'columns': [
        { 'data': 'id' },
        { 'data': 'name' },
        { 'data': 'userName' },
        { 'data': 'sexString' },
        { 'data': 'phone' },
        { 'data': 'createTime' },
        {
            'data': 'enableString',
            'render': function (data, type, row) {
                if (row.enable === 1)
                    return '<span style="color:#19be6b" >' + row.enableString + '</span>';
                else
                    return '<span style="color:#ed3f14" >' + row.enableString + '</span>';
            }
        },
        {
            'data': null,
            'render': function (data, type, row) {
                return '<a id="btn-edit" class="btn btn-success btn-xs"  title="编辑" onClick=btn_edit(' + row.id + ')><i class="fa fa-edit"></i>编辑</a>     ' +
                    '<a id="btn-edit" class="btn btn-danger btn-xs"  title="删除" onClick=btn_edit(' + row.id + ')><i class="fa fa-trash "  title="删除" style="cursor:pointer"></i>删除</a>';
            }
        },
    ],
    //datatable设置参数 http://www.datatables.club/reference/option/
    'paging': true,         //启用分页
    'lengthChange': true,   //设置每页数量
    'searching': false,
    'ordering': false,
    'info': true,
    'autoWidth': false,
    //设置中文
    'language': {
        "sProcessing": "玩命加载中...",
        "sLengthMenu": "每页显示显示 _MENU_",
        "sZeroRecords": "没有匹配结果",
        "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
        "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
        "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
        "sInfoPostFix": "",
        "sSearch": "搜索:",
        "sUrl": "",
        "sEmptyTable": "表中数据为空",
        "sLoadingRecords": "玩命加载中...",
        "sInfoThousands": ",",
        "oPaginate": {
            "sFirst": "首页",
            "sPrevious": "上页",
            "sNext": "下页",
            "sLast": "末页"
        },
        "oAria": {
            "sSortAscending": ": 以升序排列此列",
            "sSortDescending": ": 以降序排列此列"
        }
    }
});


//给行绑定选中事件
$('#userListTable tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        table.$('tr.selected').removeClass('selected');//多选则去掉
        $(this).addClass('selected');
    }
});

//搜索
$("#btn-search").on('click', function () {
    table.draw();
});

//编辑
function btn_edit(uid) {
    alert(uid);
};
 
//新增
$("#btn_add").on('click', function () { 
    var dialog = $('#userinfo-dialog');
    dialog.modal('toggle'); 
    dialog.find('.btn-primary').text('新增保存');
});

//保存修改
function save(){
    var data = {};
    data.id = $('#user-id').html();
    data.userName = $("#user-username").val();
    data.name = $("#user-name").val();
    data.phone = $("#user-phone").val();
    data.password = $("#user-password").val();
    data.sex = $('input[name="input-sex"]:checked').val();
    data.headerImg = $("#user-img")[0].src;

    if (data.headerImg.indexOf("default-user-image.jpg") != -1) {
        alert('请上传一张头像');
        return;
    }
    $.ajax({
        url: '/Account/UserList?handler=Save',
        type: 'Post',
        data: data,
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (data) {
            if (data == 'ok') {
                var dialog = $('#userinfo-dialog');
                dialog.modal('hide'); 
                table.draw();
            }
            else {
                alert(data);
            }
        },
        errer: function () {
            alert('保存失败了');
        }
    });
};