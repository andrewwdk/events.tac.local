function createCommentItem(form, path) {
    var service = new ItemService({ url: '/sitecore/api/ssc/item' });
    var obj = {
        ItemName: 'comment - ' + form.name.value,
        TemplateID: '{B40C93A5-B381-4108-8CAB-868A95EA58FF}',
        Name: form.name.value,
        Comment: form.comment.value
    };

    service.create(obj)
        .path(path)
        .execute()
        .then(function (item) {
            form.name.value = form.comment.value = '';
            window.alert('Thank you! Your message will show on the site shortly');
        })
        .fail(function (err) { window.alert(err); });

    event.preventDefault();
    return false;
}