$(document).ready(function () {

    function sendMessage() {
        var content = $('#input-message').val();

        if (content !== '') {
            randerUserMessageHtml(content);
            $('#input-message').val('');

            $.ajax({
                type: "POST",
                url: sendUrl,
                data: { 'SendContent': content },
                dataType: "json",
                /*headers: {
                },*/
                success: function (result) {
                    console.log(result);
                    if (result.isSuccess) {
                        randerBotMessageHtml(result.message.ResponseContent);
                        $('#intent').val(result.message.Intent);
                    } else {
                        alert('伺服器忙碌中，請稍後再試！');
                    }
                },
                error: function (result) {
                    alert('伺服器忙碌中，請稍後再試！');
                }
            });
        } else {
            alert('請輸入訊息');
        }
    }

    function randerUserMessageHtml(message) {
        var html = '';
        html += '<li class="right clearfix">';
        html += '   <span class="chat-img pull-right">';
        html += '       <img src="http://placehold.it/50/FA6F57/fff&text=ME" alt="User Avatar" class="img-circle" />';
        html += '   </span>';
        html += '   <div class="chat-body clearfix">';
        html += '       <div class="msg_border msg-right"><span class="arrow_r_int arrow"></span><span class="arrow_r_out arrow"></span>';
        html += '           <p class="pull-right p-text">' + message + '</p>';
        html += '       </div>';
        html += '   </div>';
        html += '</li>';

        $('#chat').append(html);
    }

    function randerBotMessageHtml(message) {
        var html = '';
        html += '<li class="left clearfix">';
        html += '   <span class="chat-img pull-left">';
        html += '       <img src="/StaticFiles/images/profile.jpg" alt="User Avatar" class="img-circle" />';
        html += '   </span>';
        html += '   <div class="chat-body clearfix">';
        html += '       <div class="msg_border msg-left"><span class="arrow_l_int arrow"></span><span class="arrow_l_out arrow"></span>';
        html += '           <p>' + message + '</p>';
        html += '       </div>';
        html += '   </div>';
        html += '</li>';

        $('#chat').append(html);
    }

    $('#btn-chat').click(function () {
        sendMessage();
    });

    $('#input-message').keypress(function (e) {
        if (e.keyCode === 13) {
            sendMessage();
        }
    });
});