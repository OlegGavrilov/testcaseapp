import React, { useState, useEffect } from 'react';
import authService from '../services/authService';
import { yupResolver } from '@hookform/resolvers/yup';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';


const Profile = () => {

    const [user, setUser] = useState(null);

    useEffect(() => {
        fetchProfile();
    }, []);

    const fetchProfile = async () => {
        try {
            const result = await authService.profile();
            setUser(result.data);
        } catch (error) {
            toast.error(error.data.message);
        }
    }

    const [isSubmitted, setIsSubmitted] = useState(false);

    const schema = Yup.object().shape({
        name: Yup.string().optional(),
        surname: Yup.string().optional(),
        phone: Yup.string().required(),
        email: Yup.string().email().required(),
    });

    const { register, handleSubmit, formState: { errors, isDirty, isValid } } = useForm({
        mode: 'all',
        resolver: yupResolver(schema),
        values: user
    });

    const handleValidSubmit = async (data) => {
        setIsSubmitted(true)
        try {
            const result = await authService.updateProfile(data);
            console.log(result);
            if (result.data) {
                fetchProfile();
            }
        } catch (error) {
            console.log(JSON.stringify(error));
            toast.error(JSON.stringify(error));
        }
        setIsSubmitted(false)
    }

    return (
        <div className="row">
            <div className="col-5">
                <h3>Current values</h3>
                <ul className="list-group">
                    <li className="list-group-item"><span className="fw-bold">UserName (login)</span> - {user?.userName}</li>
                    <li className="list-group-item"><span className="fw-bold">Name</span> - {user?.name}</li>
                    <li className="list-group-item"><span className="fw-bold">Surname</span> - {user?.surname}</li>
                    <li className="list-group-item"><span className="fw-bold">Phone</span> - {user?.phone}</li>
                    <li className="list-group-item"><span className="fw-bold">Email</span> - {user?.email}</li>
                </ul>
            </div>
            <div className="col-6 offset-1">
                <h3>New values</h3>
                <form onSubmit={handleSubmit(handleValidSubmit)}>
                    <div className="mb-3">
                        <label htmlFor="inputName" className="form-label">Name</label>
                        <input type="text" className="form-control" id="inputName" {...register('name')} />
                        <div className="form-text text-danger">
                            {errors.name && <p>{errors.name.message}</p>}
                        </div>
                    </div>
                    <div className="mb-3">
                        <label htmlFor="inputSurname" className="form-label">Surname</label>
                        <input type="text" className="form-control" id="inputSurname" {...register('surname')} />
                        <div className="form-text text-danger">
                            {errors.surname && <p>{errors.surname.message}</p>}
                        </div>
                    </div>
                    <div className="mb-3">
                        <label htmlFor="inputPhone" className="form-label">Phone</label>
                        <input type="phone" className="form-control" id="inputPhone" {...register('phone')} />
                        <div className="form-text text-danger">
                            {errors.phone && <p>{errors.phone.message}</p>}
                        </div>
                    </div>
                    <div className="mb-3">
                        <label htmlFor="inputEmail" className="form-label">Email</label>
                        <input type="email" className="form-control" id="inputEmail" {...register('email')} />
                        <div className="form-text text-danger">
                            {errors.email && <p>{errors.email.message}</p>}
                        </div>
                    </div>

                    <button type="submit" disabled={isSubmitted || !isDirty || !isValid} className="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>
    )
}

export default Profile