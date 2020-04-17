﻿using Convience.Model.Models.GroupManage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Convience.Model.Validators.GroupManage
{
    public class PositionViewModelValidator : AbstractValidator<PositionViewModel>
    {
        public PositionViewModelValidator()
        {
            RuleFor(viewmodel => viewmodel.Name).NotNull().NotEmpty()
                .WithMessage("职位名称不能为空!");

            RuleFor(viewmodel => viewmodel.Name).MaximumLength(15)
                .WithMessage("职位名称长度不能超过15！");

        }
    }
}
